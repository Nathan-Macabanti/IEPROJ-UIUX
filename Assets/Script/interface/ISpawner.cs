using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    //void JumpSpawn(Spawner spawner, bool isAttackNote);
    //void DodgeSpawn(Spawner spawner, bool isAttackNote);
    //void AttackSpawn(Spawner spawner, bool isAttackNote);
    void Spawn(Spawner spawner, BotType bot, int index, float beat);
}
