#ifndef WORLDMAP_H
#define WORLDMAP_H
#define Srow 50
#define Scolum 50
#include "stdafx.h"
#include "room.h"
#include <iostream>
#include <vector>
#include "itemBase.h"
#include "weapon.h"

class WorldMap
{
private:
    Room * _map[Srow][Scolum];
    Room * _current;
    int _currentRow;
    int _currentColum;
<<<<<<< HEAD
	std::vector<ItemBase*> _items;
public:
    int _row;
    int _colum;
	WorldMap(){}
=======
	Item * _item = 0;
public:
    int _row;
    int _colum;
    WorldMap();
	~WorldMap(){ delete _item; delete[] &_map; delete _current;}
>>>>>>> 54307607e6e67510eb5d60450a82b4e95e98c2c5
    void CreateMap();
    void AddRoom(int row, int colum, Room &room);
    Room GetRoom(int row, int colum);
    Room GetCurrentRoom();
    Room GoToRoom(int row, int colum);
};

#endif // WORLDMAP_H
