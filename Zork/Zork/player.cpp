#include "stdafx.h"
#include "player.h"

Player::Player()
{
    _health = 3;
    _inventory = new Inventory();
    _dead = false;
}
//
Inventory* Player::GetInventory()
{
    return _inventory;
}
