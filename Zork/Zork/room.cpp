#include "stdafx.h"
#include "room.h"

Room::Room()
{

}

Room::Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits)
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
}

Room::Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, std::vector<Item*> item)
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
    _items = item;
}

Room::Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits)
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
}

Room::Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, std::vector<Item*> item)
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
    _items = item;
}

std::string Room::getName()
{
    return _roomName;
}

std::string Room::getDesc()
{
    return _roomDesc;
}

std::string Room::getLongDesc()
{
    return _longDesc;
}

std::array<char, 4> Room::getExits()
{
    return _exits;
}

void Room::setName(std::string& x)
{
    _roomName = x;
}

void Room::setDesc(std::string& x)
{
    _roomDesc = x;
}

void Room::setLongDesc(std::string& x)
{
    _longDesc = x;
}

void Room::setExits(std::array<char, 4> x)
{
    _exits = x;
}

bool Room::hasExit(char x)
{
    bool temp = false;
    for(int i = 0; i < 4; i++)
    {
        if(_exits[i] == x)
        {
            temp = true;
            break;
        }
    }
    return temp;
}

Item* Room::getItem(std::string name)
{
    Item* temp = 0;
    for(int i = 0; i < _items.size(); i++)
    {
        if(_items[i]->GetName() == name)
        {
            temp = _items[i];
            break;
        }
    }
    return temp;
}

std::vector<Item*> Room::getItems()
{
    return _items;
}

bool Room::hasItem(std::string x)
{
    bool temp = false;
    std::cout<<(*_items[0]).GetName()<<std::endl;
    for(int i = 0; i < _items.size(); i++)
    {
        if((*_items[0]).GetName() == x)
        {
            temp = true;
            break;
        }
    }
    return temp;
}

bool Room::hasItems()
{
    return (_items.size() > 0) ? true : false;

}
//
std::string Room::PrintExits()
{
    std::string temp = "";
    for(int i = 0; i < 4; i++)
    {
        if(_exits[i] == 'n')
        {
            temp += "North ";
        }
        else if(_exits[i] == 'e')
        {
             temp += "East ";
        }
        else if(_exits[i] == 's')
        {
             temp += "South ";
        }
        else if(_exits[i] == 'w')
        {
             temp += "West ";
        }
        else{}
    }
    return temp;
}
