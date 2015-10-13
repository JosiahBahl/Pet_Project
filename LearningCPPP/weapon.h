#ifndef WEAPON_H
#define WEAPON_H
//#ifndef item_h
//#define item_h
#include <item.h>


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
