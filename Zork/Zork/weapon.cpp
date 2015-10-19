#include "stdafx.h"
#include <typeinfo>
#include "weapon.h"
//
Weapon::Weapon(int damage, const std::string name, const std::string desc) : _damage(damage), ItemBase(name, desc)
{

}
//
Weapon::Weapon(const Weapon& x) : _damage(x._damage), ItemBase(x)
{

}
//
std::string Weapon::Use()
{
    return "useddfsdfsd";
}

std::string Weapon::GetType()
{
	return "weapon";
}

