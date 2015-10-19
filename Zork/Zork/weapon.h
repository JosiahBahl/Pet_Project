#ifndef WEAPON_H
#define WEAPON_H
#include "stdafx.h"
#include "itemBase.h"

namespace
{
	class Weapon : public ItemBase
	{
	protected:
		int _damage;
	public:
		Weapon();
		Weapon(int damage, const std::string name, const std::string desc);
		Weapon(const Weapon& x);
		virtual std::string Use();
	};
}
#endif // WEAPON_H
