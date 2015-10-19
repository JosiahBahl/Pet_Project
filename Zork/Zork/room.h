#ifndef ROOM_H
#define ROOM_H
#include "stdafx.h"
#include <string>
#include <array>
#include <iostream>
#include "item.h"
<<<<<<< HEAD
=======
#include "weapon.h"
#include <vector>
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5

class Room
{
protected:
    std::string _roomName;
    std::string _roomDesc;
    std::string _longDesc;
    //North,East,South,West
    std::array<char, 4> _exits;
<<<<<<< HEAD
	ItemBase * _item;
=======
	Item * _item;
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
public:
    Room();
	~Room();
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits);
<<<<<<< HEAD
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, ItemBase &item);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, ItemBase &item);
=======
    Room(std::string& name, std::string& desc, std::string& lDesc, std::array<char, 4> exits, Item * item);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits);
    Room(const std::string& name, const std::string& desc, const std::string& lDesc, std::array<char, 4> exits, Item * item);
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
    std::string getName();
    std::string getDesc();
    std::string getLongDesc();
    std::array<char, 4> getExits();
<<<<<<< HEAD
    ItemBase* getItem();
=======
    Item* getItem();
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
    void setName(std::string& x);
    void setDesc(std::string& x);
    void setLongDesc(std::string& x);
    void setExits(std::array<char, 4> x);
    bool hasExit(char x);
    bool hasItem();
    std::string PrintExits();
};

#endif // ROOM_H
