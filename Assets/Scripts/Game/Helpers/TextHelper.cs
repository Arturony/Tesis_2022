using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHelper 
{
    public static string ReplaceTags(string actualString, char startingKey, char endingKey, Dictionary<string, List<string>> commandValue)
    {
        string modifiedString = actualString;

        //iterate though every character and check if we are in a tag.
        //save the index of the starting tag
        //if we are in a tag, save every character to get the command
        //when we get the full command, get the values it should take
        //save the index of the ending tag
        //create a substring withtout the command
        //insert at the starting index

        int startingIndex = 0;

        int endigIndex = 0;

        bool inTag = false;

        string actualCommand = "";

        int numberOfChar = modifiedString.Length;

        for(int i = 0; i < modifiedString.Length; i++)
        {
            char c = modifiedString[i];

            if (inTag == false && !actualCommand.Equals(""))
            {
                //we have a command and are not in a tag
                List<string> values = commandValue[actualCommand];
                string value = "";
                if (values.Count > 1)
                {
                    int indx = Random.Range(0, values.Count);
                    value = values[indx];
                    values.RemoveAt(indx);

                    commandValue[actualCommand] = values;
                }
                else if(values.Count == 1)
                    value = values[0];

                string part1 = modifiedString.Substring(0, startingIndex);
                string part2 = modifiedString.Substring(endigIndex);
                modifiedString = part1 + part2;
                modifiedString = modifiedString.Insert(startingIndex, value);

                actualCommand = "";
                i = startingIndex + value.Length;
            }

            if (c.Equals(startingKey))
            {
                startingIndex = i;
                inTag = true;
            }
            else if(c.Equals(endingKey))
            {
                endigIndex = i + 1;
                inTag = false;
            }

            if(inTag && i != startingIndex)
            {
                actualCommand += c;
            }

            
        }

        return modifiedString;
    }
}
