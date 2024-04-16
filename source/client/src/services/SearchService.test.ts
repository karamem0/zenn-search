//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import fetch from 'jest-fetch-mock';

import { SearchTarget } from '../types/Model';

import { searchIndex } from './SearchService';

test('search indexes 200 OK', () => {
  const params = {
    request: {
      target: SearchTarget.both,
      query: 'foo',
      count: 10
    },
    response: {
      status: 200,
      body: {
        value: [
          {
            '@search.score': 0.8,
            id: '2020_01_01_150000',
            value: {
              title: 'foo',
              emoji: '😊',
              content: 'foo bar baz',
              created: '2020-01-01T15:00:00Z',
              updated: '2023-01-01T15:00:00Z',
              etag: 'a9068217-ec7c-4b3b-942a-e70fe1527431'
            }
          }
        ]
      }
    }
  };
  const expected = [
    {
      score: 0.8,
      id: '2020_01_01_150000',
      title: 'foo',
      emoji: '😊',
      content: 'foo bar baz',
      published: '2020/01/01 15:00'
    }
  ];
  fetch.mockOnce(() => Promise.resolve({
    status: params.response.status,
    body: JSON.stringify(params.response.body)
  }));
  expect(searchIndex(params.request.target, params.request.query, params.request.count)).resolves.toStrictEqual(expected);
});

test('search indexes 401 Unauthorized', () => {
  const params = {
    request: {
      target: SearchTarget.both,
      query: 'foo',
      count: 10
    },
    response: {
      status: 401
    }
  };
  fetch.mockOnce(() => Promise.resolve({
    status: params.response.status
  }));
  expect(searchIndex(params.request.target, params.request.query, params.request.count)).rejects.toHaveProperty('status', 401);
});
