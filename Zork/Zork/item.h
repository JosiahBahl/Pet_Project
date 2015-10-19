#ifndef ITEM_H
#define ITEM_H
#include "stdafx.h"
#include <string>
#include "itemBase.h"

namespace
{
	class Item : public ItemBase
	{
	public:
		Item();
		Item(const std::string name, const std::string desc);
		Item(const Item& x);
		virtual std::string Use();
	};
}

#endif // ITEM_H