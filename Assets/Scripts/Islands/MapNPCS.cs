using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace GameMechanics
{
	public class MapNPCS : MonoBehaviour, DataPersistenceInterface
	{
		public string id;
		public TextMeshProUGUI LevelName;
		public SpriteRenderer spriteRenderer;
		public Sprite check;
		public Sprite cirlce;
		public Sprite Xmarker;
        private bool markComplete;
        private bool checkComplete;
		
		public void LoadData(GameData data)
        {
         
            data.gameLevels.TryGetValue(id, out markComplete);

            // Always will be true that the first level on every Island is active
            // The ships to PY and CS are only active once PS_Level1 is complete
            if (id == "PS_Level1" || id == "PY_Level1" || id == "CS_Level1")
            {
				LevelName.color = new Color(255,255,0,255);
            }
            else
            {
                // Otherwise, all NPC are set inactive/unplayable
                LevelName.color = new Color(255,0,0,255);
				spriteRenderer.sprite = Xmarker;
            }

            // If PS_Level1 is completed, set the ships active
            if (data.gameLevels.ContainsKey("PS_Level1")) // If we can access the key yet (first save)
            {
                if (data.gameLevels["PS_Level1"]) // If level 1 is complete
                {
                    if (id == "Ship1" || id == "Ship2") // On the Ship's turn, set them active
                    {
                        LevelName.color = new Color(0,0,255,255);
						spriteRenderer.sprite = check;
                    }
                }
            }

            // For PseudoIsland, if Level is complete, set Level + 1 to active
            if (data.gameLevels.ContainsKey("PS_Level1")) // Access the key values yet
            {
                // Ignore Python and CSharp NPC
                if (id.Contains("PS_Level") && !id.Contains("Ship"))
                {
                    // Loop through all the levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("PS_Level" + i, out checkComplete);

                        if (checkComplete) // If level exists and was complete, set next level
                        {
							if(id == ("PS_Level" + (i)) || id == "PS_Level1") {
								LevelName.color = new Color(0,0,255,255);
								spriteRenderer.sprite = check;
							}else if (id == ("PS_Level" + (i + 1))) // Next level id with current
                            {
								spriteRenderer.sprite = cirlce;
                                LevelName.color = new Color(255,225,0,255);
                            }else{
								LevelName.color = new Color(255,0,0,255);
								spriteRenderer.sprite = Xmarker;
							}
                        }
                    }
                }
            }
			if (data.gameLevels.ContainsKey("PY_Level1") || data.gameLevels.ContainsKey("CS_Level1"))
            {
                // Separate out the Python and CSharp NPC based on ID
                if (id.Contains("PY_Level") && !id.Contains("Ship"))
                {
                    // Loop through the Python Levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("PS_Level" + i, out checkComplete);

                        if (checkComplete) // PS Level exists and was complete
                        {
                            // Test if level exists
                            data.gameLevels.TryGetValue("PY_Level" + i, out checkComplete);

                            if (checkComplete) // PY Level exists and was complete
                            {
                                if(id == ("PY_Level" + (i))){
									LevelName.color = new Color(0,0,255,255);
									spriteRenderer.sprite = check;
								}else if (id == ("PY_Level" + (i + 1))) // Next level id with current
								{
									spriteRenderer.sprite = cirlce;
									LevelName.color = new Color(255,225,0,255);
								}else{
									LevelName.color = new Color(255,0,0,255);
									spriteRenderer.sprite = Xmarker;
								}
                            }
                        }
                    }
                }

                if (id.Contains("CS_Level") && !id.Contains("Ship"))
                {
                    // Loop through the CSharp Levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("CS_Level" + i, out checkComplete);

                        if (checkComplete) // PS Level exists and was complete
                        {
                            // Test if level exists
                            data.gameLevels.TryGetValue("CS_Level" + i, out checkComplete);

                            if (checkComplete) // CS Level exists and was complete
                            {
                                if(id == ("CS_Level" + (i))){
									LevelName.color = new Color(0,0,255,255);
									spriteRenderer.sprite = check;
								}else if (id == ("CS_Level" + (i + 1))) // Next level id with current
								{
									spriteRenderer.sprite = cirlce;
									LevelName.color = new Color(255,225,0,255);
								}else{
									LevelName.color = new Color(255,0,0,255);
									spriteRenderer.sprite = Xmarker;
								}
                            }
                        }
                    }
                }
            }
        }
		
		public void SaveData(GameData data)
        {
           
        }
	}
}
