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

<<<<<<< HEAD
Room::Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, ItemBase &item)
=======
Room::Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, Item * item)
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
<<<<<<< HEAD
	_item = &item;
=======
    _item = item;
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
}

Room::Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits)
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
}

<<<<<<< HEAD
Room::Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, Item &item)
=======
Room::Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, Item * item)
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
{
    _roomName = name;
    _roomDesc = desc;
    _longDesc = lDesc;
    _exits = exits;
<<<<<<< HEAD
	_item = item;
=======
    _item = item;
}

Room::~Room()
{

>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
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

<<<<<<< HEAD
Item Room::getItem()
{
    return _item;
}

bool Room::hasItem()
{
	return (_item != NULL) ? true : false;
=======
Item* Room::getItem()
{
    return _item;
}

bool Room::hasItem()
{
	return (_item) ? true : false;
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
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
