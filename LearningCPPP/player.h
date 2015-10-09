#ifndef PLAYER_H
#define PLAYER_H
#include <inventory.h>
#include <string>


class Player
{
private:
    //
    int _health;
    //
    Inventory _inventory;
    //
    bool _dead;
public:
    Player();
    std::string PrintInventory();
};

#endif // PLAYER_H
