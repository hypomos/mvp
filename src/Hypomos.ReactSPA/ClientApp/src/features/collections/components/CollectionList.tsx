import { RootState } from 'MyTypes';
import React from 'react';
import { connect } from 'react-redux';

import { loadCollectionsAsync } from '../actions';

import * as selectors from '../selectors';

import CollectionListItem from './CollectionListItem';

const mapStateToProps = (state: RootState) => ({
  isLoading: state.collections.isLoadingCollections,
  collections: selectors.getCollections(state),
});

const dispatchProps = {
  loadCollectionsAsync: loadCollectionsAsync.request
};

type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps;

class CollectionList extends React.Component<Props> {
  componentDidMount() {
    this.props.loadCollectionsAsync();
  }

  render() {
    const { collections } = this.props;
    return (
      <ul>
        {collections.map(collection => (
          <li key={collection.id}>
            <CollectionListItem collection={collection} />
          </li>
        ))}
      </ul>
    );
  }
}
// const CollectionList: React.FC<Props> = ({
//   isLoading,
//   collections: collections = [],
// }) => {
//   if (isLoading) {
//     return <p style={{ textAlign: 'center' }}>Loading collections...</p>;
//   }

//   if (collections.length === 0) {
//     return (
//       <p style={{ textAlign: 'center' }}>
//         No collections yet, please create new...
//       </p>
//     );
//   }

//   return (
//     <ul style={getStyle()}>
//       {collections.map(collection => (
//         <li key={collection.id}>
//           <CollectionListItem collection={collection} />
//         </li>
//       ))}
//     </ul>
//   );
// };

// const getStyle = (): React.CSSProperties => ({
//   textAlign: 'left',
//   margin: 'auto',
//   maxWidth: 500,
// });

export default connect(
  mapStateToProps,
  dispatchProps
)(CollectionList);