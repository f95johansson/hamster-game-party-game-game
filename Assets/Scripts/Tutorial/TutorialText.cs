using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText {

	public static string[][] Tutorial = new string[][] {
        new string[] {
            "Hi! Welcome to The Hamster Ball Game.\n",
            "Go ahead a try to make the hamster get the coin.\nYou can drag and place the fan, it will push the ball",
            "When you think the hamster will complete the track you can press GO! to test your solution.",
        },
        new string[] {
            "Use multiple fans when necessary.",
        },
        new string[] {
            "Hey. I see you got a carrot now too.",
            "Maybe it will give the hamster reasons to turn. Just drag and drop it to try it.",
            "Use the undo button if you feel like you made a misstake. Dragging and object to the trashcan works too."
        },
        new string[] {
            "Often a combinations of carrots and fans are necessary. Are you up for the challenge?",
            "Some more text"
        }
    };

    public static string[] Completed = new string[] {
        "Great! I think you got the hang of this. You are ready to explore the rest of the levels now!"
    };
}
