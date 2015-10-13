#include "inventory.h"
//
Inventory::Inventory()
{
    _inventory = std::vector<Item*>();
}
//
std::string Inventory::AddItem(Item *item)
{
    std::string temp = "You already have a "+item->GetName()+" in your invenotry. You can only have one item at a time.";
    if(!Contains(item->GetName()))
    {
        _inventory.push_back(item);
        temp = "Added "+item->GetName()+" to inventory.";
    }
    else
    {

    }
    return temp;
}
//
std::string Inventory::RemoveItem(std::string name)
{
    std::string temp = "You don't have a "+name+" in your invenotry to drop.";
    if(Contains(name))
    {
        _inventory.erase(_inventory.begin()+GetIndexOf(name));
        temp = "You dropped "+name+" from your inventory.";
    }
    else
    {

    }
    return temp;
}
//
std::string Inventory::UseItem(std::string name)
{

}
//
Item* Inventory::GetItem(std::string name)
{

}
//
int Inventory::GetSize()
{
    return _inventory.size();
}
//
bool Inventory::Contains(std::string name)
{
    bool temp = false;
    for(int i = 0; i < _inventory.size(); i++)
    {
        if(_inventory[i]->GetName() == name)
        {
            temp = true;
            break;
        }
        else{}
    }
    return temp;
}
//
int Inventory::GetIndexOf(std::string name)
{
    int temp = -1;
    for(int i = 0; i < _inventory.size(); i++)
    {
        if(_inventory[i]->GetName() == name)
        {
            temp = i;
            break;
        }
        else{}
    }
    return temp;
}
