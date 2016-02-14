using UnityEngine;
using System.Collections;

/**
Created By: Ivan
TODO: change usages of CharacterBase to the new character base class
*/
public class Powerup{

    protected enum PowerupType
    {
        JUMP, HEALTH, ENERGY, DAMAGE, SPEED 
    }

    protected PowerupType[] mods;
    protected float[] modAmount;
    protected bool applied = false;
    
    public void applyPowerup(Player character)
    {
        if(!applied)
        for(int i = 0; i < mods.Length; i++)
        switch(mods[i])
        {
            case PowerupType.JUMP:
                    character.jumpHeight = (character.jumpHeight + modAmount[i]);
                break;
            case PowerupType.HEALTH:
                    character.maxHealth = (int)(character.maxHealth + modAmount[i]);
                break;
            case PowerupType.ENERGY:
                    character.maxEnergy = (int)(character.maxEnergy + modAmount[i]);
                 break;
            case PowerupType.DAMAGE:
                    character.damage = (int)(character.damage + modAmount[i]);
                break;
            case PowerupType.SPEED:
                    character.speed = (character.speed + modAmount[i]);
                break;
        }
        applied = !applied;
    }
    public void removePowerup(Player character)
    {
        if (applied)
            for (int i = 0; i < mods.Length; i++)
                switch (mods[i])
                {
                    case PowerupType.JUMP:
                        character.jumpHeight = (character.jumpHeight - modAmount[i]);
                        break;
                    case PowerupType.HEALTH:
                        character.maxHealth = (int)(character.maxHealth - modAmount[i]);
                        break;
                    case PowerupType.ENERGY:
                        character.maxEnergy = (int)(character.maxEnergy - modAmount[i]);
                        break;
                    case PowerupType.DAMAGE:
                        character.damage = (int)(character.damage - modAmount[i]);
                        break;
                    case PowerupType.SPEED:
                        character.speed = (character.speed - modAmount[i]);
                        break;
                }
        applied = !applied;
    }
    public string getPowerupAttributes()
    {
        string message = "";
        
        for(int i = 0; i < mods.Length; i++)
        {
            message += mods[i].ToString() + " : " + modAmount[i] + "\n"; 
        }
        return message;
    }
}
