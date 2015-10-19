#ifndef ITEMBASE_H
#define ITEMBASE_H
#include "stdafx.h"
#include <string>

class ItemBase
{
protected:
    std::string _name;
    std::string _desc;
public:
	ItemBase(){}
	ItemBase(const std::string name, const std::string desc);
	ItemBase(const  ItemBase& x);
    virtual std::string Use() = 0;
    std::string GetName();
};

#endif // ITEMBASE_H
