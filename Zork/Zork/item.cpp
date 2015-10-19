#include "stdafx.h"
#include "item.h"
//
Item::Item(const std::string name, const std::string desc) : ItemBase(name, desc)
{

}
//
Item::Item(const Item& x) : ItemBase(x)
{

}
//
std::string Item::Use()
{
	return "useddfsdfsd";
}