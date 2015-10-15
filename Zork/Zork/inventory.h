#ifndef INVENTORY_H
#define INVENTORY_H
#include "stdafx.h"
#include "item.h"
#include <vector>

class Inventory
{
private:
   std::vector<Item*> _inventory;
public:
    Inventory();
    std::string AddItem(Item *item);
    std::string RemoveItem(std::string name);
    std::string UseItem(std::string name);
    Item* GetItem(std::string name);
    int GetSize();
    bool Contains(std::string name);
    int GetIndexOf(std::string name);
};

#endif // INVENTORY_H
