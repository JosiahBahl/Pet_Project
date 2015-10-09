#ifndef ITEM_H
#define ITEM_H
#include <string>

class Item
{
protected:
    std::string _name;
    std::string _desc;
public:
    Item();
    Item(std::string name, std::string desc);
    virtual std::string Use() = 0;
};

#endif // ITEM_H
