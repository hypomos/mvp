const pathsMap = {
    loading: () => '/',
    home: () => '/home',
    app: () => '/app',
    callback: () => '/callback',
    
    items: () => '/app/items',
    
    collections: () => '/app/collections',
    addCollection: () => '/app/collection/add',
    viewCollection: (collectionId: string) => `/app/collections/${collectionId}`,
    editCollection: (collectionId: string) => `/app/collections/${collectionId}/edit`,
    
    cleaning: () => '/app/cleaning'
};

type PathsMap = typeof pathsMap;

export const getPath = <TRoute extends keyof PathsMap>(
    route: TRoute,
    ...params: Parameters<PathsMap[TRoute]>
) => {
    const pathCb: (...args: any[]) => string = pathsMap[route];

    return pathCb(...params);
};