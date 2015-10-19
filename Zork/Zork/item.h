#ifndef ITEM_H
#define ITEM_H
#include "stdafx.h"
#include <string>
#include "itemBase.h"

namespace
{
<<<<<<< HEAD
	class Item : public ItemBase
	{
	public:
		Item();
		Item(const std::string name, const std::string desc);
		Item(const Item& x);
		virtual std::string Use();
	};
}
=======
protected:
    std::string _name;
    std::string _desc;
public:
    Item();
    Item(const std::string name, const std::string desc);
    virtual std::string Use() = 0;
	virtual std::string GetType() = 0;
    std::string GetName();
};
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5

#endif // ITEM_H