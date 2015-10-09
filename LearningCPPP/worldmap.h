#ifndef WORLDMAP_H
#define WORLDMAP_H
#define Srow 25
#define Scolum 25
#include <room.h>
#include <iostream>

class WorldMap
{
private:
    Room _map[Srow][Scolum];
    Room _current;
    int _currentRow;
    int _currentColum;
public:
    int _row = Srow;
    int _colum = Scolum;
    WorldMap();
    void CreateMap();
    void AddRoom(int row, int colum, Room &room);
    Room GetRoom(int row, int colum);
    Room GetCurrentRoom();
    Room GoToRoom(int row, int colum);
};

#endif // WORLDMAP_H
