import { Collection } from 'MyModels';
import React from 'react';
import areEqual from 'fast-deep-equal';
import { connect } from 'react-redux';

import { deleteCollectionAsync } from '../actions';
import { getPath } from '../../../router-paths';
//import FlexRow from '../../../components/FlexRow';
import { Link } from 'react-router-dom';

const dispatchProps = {
  deleteCollection: deleteCollectionAsync.request,
};

type Props = typeof dispatchProps & {
  collection: Collection;
};

const CollectionListItem = React.memo<Props>(({ collection, deleteCollection }) => {
  return (
    <div className="max-w-sm rounded overflow-hidden shadow-lg">
      <div className="px-6 py-4">
        <div className="font-bold text-xl mb-2" style={getStyle()}>{collection.title}</div>
        {/* <p className="text-gray-700 text-base">
        </p> */}
      </div>
      
      <div className="px-6 py-4">
        <Link to={getPath('viewCollection', collection.id)} className="font-bold py-2 px-4 m-2 rounded bg-blue-500 text-white hover:bg-blue-700">View</Link>
        <Link to={getPath('editCollection', collection.id)} className="py-2 px-4 m-2 rounded bg-gray-500 text-white hover:bg-gray-700">Edit</Link>
        <a
          className="py-2 px-4 m-2 rounded bg-red-500 text-white hover:bg-red-700"
          onClick={() => deleteCollection(collection)}
          style={{ color: 'darkred' }}
        >
          Delete
        </a>
      </div>
    </div>

  );
}, areEqual);

const getStyle = (): React.CSSProperties => ({
  overflowX: 'hidden',
  textOverflow: 'ellipsis',
  width: '300px',
});

export default connect(
  null,
  dispatchProps
)(CollectionListItem);