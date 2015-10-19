#include "stdafx.h"
#include "itemBase.h"
//
ItemBase::ItemBase(const std::string name, const std::string desc) : _name(name), _desc(desc)
{

}
//
ItemBase::ItemBase(const ItemBase& x) : _name(x._name), _desc(x._desc)
{

}
//
std::string ItemBase::GetName()
{
    return _name;
}
//

