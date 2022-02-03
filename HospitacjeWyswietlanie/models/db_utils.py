from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker


class UrlHandler:
    __defaultURL__ = 'mysql+mysqlconnector://root:root@localhost:3306/hospitalisationplaner'
    # __defaultURL__ = 'datasource=hospitator-db.mysql.database.azure.com;port=3306;username=ad_hospitator;password=gh4w5y239Aws324;database=hospitator;'

    def __init__(self):
        self._url = self.__defaultURL__

    @property
    def url(self):
        return self._url

    def setURL(self, new_url):
        self._url = new_url



url = UrlHandler()

def make_session():
    engine = create_engine(url.url, echo=False, pool_size=100)
    Session = sessionmaker(bind=engine)
    return Session()


def get_all(entity):
    session = make_session()
    return session.query(entity)
