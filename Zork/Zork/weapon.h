#ifndef WEAPON_H
#define WEAPON_H
#include "stdafx.h"
#include "item.h"


class Weapon: public Item
{
private:
    int _damage;
public:
    Weapon();
    Weapon(int damage, const std::string name, const std::string desc);
    virtual std::string Use();
};
#endif // WEAPON_H
