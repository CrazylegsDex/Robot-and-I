/*
 * This file contains an interface that handles the passing
 * back and forth of data to the DataPersistenceManager.
 * This interface is implemented in each of the scripts
 * that will use saving and loading of data.
 * 
 * Author: Robot and I Team
 * Date: 01/21/2023
 */

namespace GameMechanics
{
    public interface DataPersistenceInterface
    {
        void LoadData(GameData data);
        void SaveData(GameData data);
    }
}