#ifndef PLAYER_H
#define PLAYER_H
#include "stdafx.h"
#include "inventory.h"
#include <string>

class Player
{
private:
    //
    int _health;
    //
    Inventory * _inventory;
    //
    bool _dead;
public:
    Player();
    Inventory* GetInventory();
};

#endif // PLAYER_H
