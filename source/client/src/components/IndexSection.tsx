//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { SearchIndexData } from '../types/Model';

import Presenter from './IndexSection.presenter';

interface IndexSectionProps {
  indexes: SearchIndexData[]
}

function IndexSection(props: Readonly<IndexSectionProps>) {

  const {
    indexes
  } = props;

  return (
    <Presenter indexes={indexes} />
  );

}

export default IndexSection;
