# Cybersecurity Awareness Chatbot

A Windows Forms desktop application that educates users on cybersecurity topics.

## Features
- Cybersecurity chatbot with keyword and sentiment detection
- Task Assistant with MySQL database storage
- Cybersecurity Quiz with 12 questions
- NLP simulation for natural user input
- Activity Log to track bot actions

## Requirements
- Windows OS
- .NET 8.0
- MySQL Server (running locally)
- Visual Studio 2022

## How to Run
1. Clone the repository
2. Open `CybersecurityAwarenessBotGUI.sln` in Visual Studio
3. Open MySQL Workbench and run the SQL in the `Database` folder
4. Update the password in `Services/TaskService.cs`
5. Press F5 to build and run

## Database Setup
Run this in MySQL Workbench:
CREATE DATABASE IF NOT EXISTS CyberBotDB;
USE CyberBotDB;
CREATE TABLE IF NOT EXISTS Tasks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT,
    ReminderDate DATE NULL,
    IsCompleted BOOLEAN DEFAULT FALSE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);