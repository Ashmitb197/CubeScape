import io
import sys
import cv2
import mediapipe as mp
import socket
import csv
import time
import os

# Set UTF-8 encoding for stdout and stderr
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8')

# Ensure CSV file exists
csv_filename = "gesture_data.csv"
if not os.path.exists(csv_filename):
    with open(csv_filename, mode="w", newline="", encoding="utf-8") as file:
        writer = csv.writer(file)
        writer.writerow(["Timestamp", "Frame Number", "Gesture"])  # Write header

# Initialize Mediapipe
mp_hands = mp.solutions.hands
hands = mp_hands.Hands(min_detection_confidence=0.7, min_tracking_confidence=0.7)
mp_draw = mp.solutions.drawing_utils

# Initialize webcam
cap = cv2.VideoCapture(0)

# Initialize Socket
server_ip = "127.0.0.1"
server_port = 8080
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

frame_count = 0  # Initialize frame count

# Function to detect gestures
def detect_gesture(landmarks):
    if landmarks[8].y < landmarks[6].y:  # Index finger up
        return "W"
    if landmarks[12].y < landmarks[10].y:  # Middle finger up
        return "A"
    if landmarks[16].y < landmarks[14].y:  # Ring finger up
        return "S"
    if landmarks[20].y < landmarks[18].y:  # Pinky finger up
        return "D"
    return None

# Start the loop
try:
    while True:
        success, frame = cap.read()
        if not success:
            print("❌ ERROR: Could not read frame from webcam")
            break

        frame_count += 1  # Increment frame count
        timestamp = time.strftime("%Y-%m-%d %H:%M:%S")  # Get current timestamp

        # Convert frame to RGB
        rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        result = hands.process(rgb_frame)

        detected_gesture = None  # Default value

        if result.multi_hand_landmarks:
            for hand_landmarks in result.multi_hand_landmarks:
                mp_draw.draw_landmarks(frame, hand_landmarks, mp_hands.HAND_CONNECTIONS)

                # Detect gesture
                detected_gesture = detect_gesture(hand_landmarks.landmark)
                if detected_gesture:
                    sock.sendto(detected_gesture.encode(), (server_ip, server_port))  # Send to Unity

        # Debug print to confirm what's being saved
        print(f"✅ Saving to CSV: {timestamp}, {frame_count}, {detected_gesture or 'None'}")

        # Save gesture data to CSV
        with open(csv_filename, mode="a", newline="", encoding="utf-8") as file:
            writer = csv.writer(file)
            writer.writerow([timestamp, frame_count, detected_gesture or "None"])  # Ensure None is recorded

        # Show the frame
        cv2.imshow("Hand Gesture Control", frame)

        # Exit when 'q' is pressed
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

except Exception as e:
    print(f"❌ An error occurred: {e}")

finally:
    # Cleanup
    cap.release()
    cv2.destroyAllWindows()
    sock.close()
    print(f"📂 Gesture data saved in {os.path.abspath(csv_filename)}")

