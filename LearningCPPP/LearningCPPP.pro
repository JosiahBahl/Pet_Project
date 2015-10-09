TEMPLATE = app
CONFIG += console c++11
CONFIG -= app_bundle
CONFIG -= qt

SOURCES += main.cpp \
    parser.cpp \
    room.cpp \
    worldmap.cpp \
    item.cpp \
    weapon.cpp \
    player.cpp \
    inventory.cpp

HEADERS += \
    parser.h \
    room.h \
    worldmap.h \
    item.h \
    weapon.h \
    player.h \
    inventory.h

