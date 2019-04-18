using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameRules {
    void resetGame();

    bool isHeatEnabled();
}
