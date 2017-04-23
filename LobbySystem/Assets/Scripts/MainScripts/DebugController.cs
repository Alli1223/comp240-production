using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class DebugController : MonoBehaviour
{
    //Delare the singleton
    public static DebugController instance;
    //File path variables
    private string defaultFilePath, fileName = "/firelock-multiplayer.txt", currentDirectory = Environment.CurrentDirectory;

    private void Awake() //Initalize the debugger
    {
        instance = this; //Initialize the singleton
        defaultFilePath = currentDirectory + fileName; //Add the file name to the default path
        Debug.Log(defaultFilePath);
        CreateDebugFile(); //Create the debug file if the player the program is running in admin 
    }

    private bool CheckFilePathValidity()
    {
        if (defaultFilePath != null && File.Exists(defaultFilePath)) //If the file path isn't null and is a valid file path then say it is valid
        {
            return true;
        }
        else
        {
            return false;
        }
    } //Call me to check the validity of the file path

    private void CreateDebugFile() //This will create the debug file in the selected directory for the game
    {
        try 
        {
            // Check if the file exists in the defaut directory
            if (!File.Exists(currentDirectory + fileName))
            {
                // Create a file and write to the default directory
                string textToWrite = "Debug file created!" + Environment.NewLine; //Log the creation of the document in the debug logger
                File.WriteAllText(defaultFilePath, textToWrite);
            }
        }
        catch //If there has been an error in the try (The most likely error will be the folder existing or not or write permissions then try another folder)
        {
            // If there has been an error finding or creating the file in the specified directory, create it in the my documents folder
            if (!File.Exists(currentDirectory + fileName))
            {
                // Create a file to write to.
                string textToWrite = "Debug file created in alternate directory" + Environment.NewLine; //Let the user know this is not the default place for the Debug log
                File.WriteAllText(defaultFilePath, textToWrite);
            }
        }


    }

    public void WriteTextLogToExistingFile(string inputTextHere, Type scriptCalledFrom) //This will write text to an existing file
    {
        if (CheckFilePathValidity() == true) //if there file path is valid
        {
            string dateAndTime = DateTime.Now.ToString(); //Get current date and time to stamp onto the debug log
            File.AppendAllText(defaultFilePath, "Debug Log: " + inputTextHere + "     Script Written from: " + scriptCalledFrom.ToString() + "     " + dateAndTime + Environment.NewLine); //Add onto the text file instead of create a new one
        }
    }

    public void WriteErrorToExistingFile(Exception exception, String inputTextHere, Type scriptCalledFrom) //This will write an error to an existing file
    {
        if (CheckFilePathValidity() == true) //if there file path is valid
        {
            string dateAndTime = DateTime.Now.ToString(); //Get current date and time to stamp onto the debug log
            string error = exception.ToString(); //Convert the error to a string
            File.AppendAllText(defaultFilePath, "ERROR: " + error.Substring(0, 38) + "     " + inputTextHere + "     Script Written from: " + scriptCalledFrom.ToString() + "     " + dateAndTime + Environment.NewLine); //Add onto the text file instead of create a new one
        }
    }

    public void SetNewDefaultFilePath(string filepath) //Set a new file path via code if ever needed
    {
        defaultFilePath = filepath + fileName;
    }
}
