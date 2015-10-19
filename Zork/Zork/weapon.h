#ifndef WEAPON_H
#define WEAPON_H
#include "stdafx.h"
#include "itemBase.h"

namespace
{
<<<<<<< HEAD
	class Weapon : public ItemBase
	{
	protected:
		int _damage;
	public:
		Weapon();
		Weapon(int damage, const std::string name, const std::string desc);
		Weapon(const Weapon& x);
		virtual std::string Use();
=======
	class Weapon : public Item
	{
	private:
		int _damage;
	public:
		Weapon(){}
		Weapon(int damage, const std::string name, const std::string desc);
		virtual std::string Use();
		virtual std::string GetType();
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
	};
}
#endif // WEAPON_H
