#include "stdafx.h"
#include "worldmap.h"
#include "weapon.cpp"
WorldMap::WorldMap()
{

}

void WorldMap::CreateMap()
{
	_item = new Weapon(0, "Penny", "A shiny penny");
    std::array<char, 4> exits = {'n','0','0','0'};
    //
    _map[0][25] = new Room("Entrance",
                       "An entrance way. You have an exit to the north",
                       "The walls are made out of thick stone, there is some mold on the walls",
                       exits, _item);
    //
	exits = { { 'n', 'e', 's', 'w' } };
    //
    _map[1][25] = new Room("Foyer",
                       "A large foyer, there is a chandalier hanging fromt he ceiling and two staircases that lead to the upper level. You have exits to the north, east, south and west.",
                       "There is a huge chandalier in the middle of the foyer, two grand stair cases lead to the upper level. There are doors to the left and right. Behind you is the entry way.",
                       exits);
    //
	exits = { { '0', '0', '0', 'w' } };
    //
	_item = new Weapon(1, "Broom", "A shitty broom");
    //
    _map[1][24] = new Room("Broomcloset",
                       "You enter narrow closet filled with brooms. There is only the north exit back to the foyer",
                       "Its to dark in the broom closet to really see, you do see some brooms on the near wall.",
                       exits, _item);
    //
    _current = _map[0][25];
    _row = 0;
    _colum = 25;
    _currentRow = 0;
    _currentColum = 25;
}

void WorldMap::AddRoom(int row, int colum, Room &room)
{
    if((row < Srow) && (colum < Scolum))
    {
       _map[row][colum] = &room;
    }
    else
    {

    }
}

Room WorldMap::GetRoom(int row, int colum)
{
    if((row < Srow) && (colum < Scolum))
    {
        return *_map[row][colum];
    }
    else
    {
        return *_current;
    }
}

Room WorldMap::GetCurrentRoom()
{
    return *_current;
}

Room WorldMap::GoToRoom(int row, int colum)
{
    if((row < Srow) && (colum < Scolum))
    {
        _current = _map[row][colum];
        _row = row;
        _colum = colum;
        _currentRow = row;
        _currentColum = colum;
        return *_current;
    }
    else
    {
        return *_current;
    }
}