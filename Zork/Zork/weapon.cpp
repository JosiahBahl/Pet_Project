#include "stdafx.h"
#include "weapon.h"
//
Weapon::Weapon(int damage, const std::string name, const std::string desc) : _damage(damage), Item(name, desc)
{

}

std::string Weapon::Use()
{
    return "useddfsdfsd";
}

