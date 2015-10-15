#include "stdafx.h"
#include "item.h"
//
Item::Item(const std::string name, const std::string desc) : _name(name), _desc(desc)
{

}

//
std::string Item::GetName()
{
    return _name;
}
