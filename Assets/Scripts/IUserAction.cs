// UI界面与场景管理器通信的接口
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, PAUSE, START}
public enum ActionMode { PHYSIC, KINEMATIC, NOTSET }
 
public interface IUserAction {
    void GameOver();
    GameState getGameState();
    void setGameState(GameState gs);
    int GetScore();
    void hit(Vector3 pos);
    ActionMode getMode();
    void setMode(ActionMode m);
}