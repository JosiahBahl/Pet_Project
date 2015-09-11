#ifndef MAIN_H
#define MAIN_H

#include <QtLocation>
#include <QtPositioning>

class Main : QObject
{
    Q_OBJECT
public:
    Main(QObject *parent = 0): QObject(parent){}
private slots:
    void positionUpdated(const QGeoPositionInfo &info){}
};

#endif // MAIN_H
