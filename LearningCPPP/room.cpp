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

