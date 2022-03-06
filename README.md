# APIToyRobot

An Api Solution using swagger that allows you to move and turn a robot

All you need is Visual studio 2019 and run the app. Tests have been added for some of the classes.

1. First Initialize the Robot with the endpoint "Place" 
 # e.g.{
 # "id": "",
 # "verticalAxis": 3,
 # "horizontalAxis": 2,
 # "direction": "North"
 # }
3. Use the identifier to set the position of the robot with the endpoint "Place" the next time
4. To check if position has been set use "Report"
5. A robot will only move in the direction it is facing. For this use "Move"
6. To rotate a robot Anticlockwise use "Left" otherwise use "Right"
7. Multiple robots can be initialized and run randomly

Memory object acts as a random storage... this could be a database or blob or anything, I have used the most convenient one.
This is to support multiple robots through the API.
