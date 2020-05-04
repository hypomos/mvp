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
    <div>
      <div style={getStyle()}>{collection.title}</div>
      <div>
        <Link to={getPath('viewCollection', collection.id)}>View</Link>
        <Link to={getPath('editCollection', collection.id)}>Edit</Link>
        <div
          className="link"
          onClick={() => deleteCollection(collection)}
          style={{ color: 'darkred' }}
        >
          Delete
        </div>
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