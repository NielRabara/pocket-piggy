#!/usr/bin/env python3
"""
Debug test - minimal script to test integration
"""

import sys
import os

print("# Debug Test Results")
print(f"Python version: {sys.version}")
print(f"Script path: {__file__}")
print(f"Working directory: {os.getcwd()}")
print(f"Arguments: {sys.argv}")

if len(sys.argv) > 1:
    csv_file = sys.argv[1]
    print(f"CSV file: {csv_file}")
    print(f"CSV exists: {os.path.exists(csv_file)}")
    
    if os.path.exists(csv_file):
        try:
            with open(csv_file, 'r') as f:
                lines = f.readlines()
            print(f"CSV lines: {len(lines)}")
            if lines:
                print(f"First line: {lines[0].strip()}")
        except Exception as e:
            print(f"Error reading CSV: {e}")

print("Debug test completed successfully!")
