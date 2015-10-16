#ifndef ITEM_H
#define ITEM_H
#include "stdafx.h"
#include <string>

class Item
{
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

#endif // ITEM_H
