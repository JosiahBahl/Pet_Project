#include "item.h"
//
Item::Item()
{

}
//
Item::Item(std::string name, std::string desc)
{
    _name = name;
    _desc = desc;
}
//
std::string Item::GetName()
{
    return _name;
}
