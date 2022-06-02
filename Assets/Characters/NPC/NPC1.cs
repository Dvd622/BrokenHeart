using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        lines = new string[] {"Player: Hello, I am hoping that you could assist me. Is this Willow’s Peak?",
                                "Male and Female Villager remain silent: ………….", 
                                "Player: Listen I just need to ask a couple of questions and then I’ll leave you be.",
                                "*Villagers stare at the player*", 
                                "Player: My name is Ed Wójcik, I am looking into the disappearance of my parents.",
                                "I heard rumours that they were sighted around this area, do these people look familiar to you?",
                                "*Shows picture to villagers*",
                                "Female Villager: The practitioners", 
                                "Player: so you do know them. Do you know what happened to them?", 
                                "Male Villager: Taken…..along with…….others",
                                "Female Villager: Jeremiah please, don’t strain yourself", 
                                "Player: What do you mean taken? Where were they taken to?",
                                "Female Villager: Please forgive my husband, ever since he escaped the Institution his mind has been riddled. He can barely speak a coherent sentence.",
                                "Player: Please, where have they taken my parents?",
                                "Female Villager: To Lornechester Institution",
                                "*Player looks and sees the abandoned hospital on the peak of the cliff*",
                                "Player: Why were they taken there?",
                                "Female villager: I have no clue. All I know is that when they took Jeremiah he came back broken instead of healed",
                                "Player: You mentioned others, what others?",
                                "Female villager: The villagers. Women, men and children taken during the black of night to never appear again",
                                "Player: If that’s where my parents are then I must go there",
                                "Female villager: No… there is nothing but death there, if you value your life forget about your parents and turn back now",
                                "Player: I can’t, I need to find them"};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
