#-------------------------------------------------
#
# Project created by QtCreator 2015-09-03T09:21:29
#
#-------------------------------------------------

QT       += core gui location

TARGET = UnityGPSPlugin
TEMPLATE = lib
CONFIG += plugin

DESTDIR = $$[QT_INSTALL_PLUGINS]/generic

SOURCES += main.cpp

HEADERS += main.h
DISTFILES += UnityGPSPlugin.json \
    mainQML.qml

unix {
    target.path = /usr/lib
    INSTALLS += target
}
