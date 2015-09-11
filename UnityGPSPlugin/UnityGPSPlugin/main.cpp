#include "main.h"


Main::Main()
{
    //QGeoServiceProvider _service = new QGeoServiceProvider();
   // QGeoCodingManager * _manager = _service.geocodingManager();
    QGeoPositionInfoSource * _source = QGeoPositionInfoSource::createDefaultSource(this);
    if(_source)
    {
        connect(_source, SIGNAL(positionUpdated(QGeoPositionInfo)), this, SLOT(positionUpdated(QGeoPositionInfo)));
        _source->setUpdateInterval(5000);
        _source->startUpdates();
    }
}

Main::positionUpdated(const QGeoPositionInfo &info)
{
    qDebug()<<"Position: "<<info;
}

int main()
{
    Main _test = new Main();
    int i = 1;
    while(i)
    {
        cin >> i;
    }
    return 0;
}
