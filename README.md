# からめもぶろぐ。記事検索

[![.github/workflows/workflow.yml](https://github.com/karamem0/zenn-search/actions/workflows/workflow.yml/badge.svg)](https://github.com/karamem0/zenn-search/actions/workflows/workflow.yml)
[![codecov](https://codecov.io/gh/karamem0/zenn-search/graph/badge.svg?token=UY4ZU4E73I)](https://codecov.io/gh/karamem0/zenn-search)
[![License](https://img.shields.io/github/license/karamem0/zenn-search.svg)](https://github.com/karamem0/zenn-search/blob/main/LICENSE)

Azure AI Search および Azure Cosmos DB for MongoDB vCore を使ってベクター検索をするサンプルです。

- [Azure AI Search でベクトル クエリを作成する](https://learn.microsoft.com/ja-jp/azure/search/vector-search-how-to-create-index)
- [Azure Cosmos DB for MongoDB 仮想コアの埋め込みでベクター検索を使用する](https://learn.microsoft.com/ja-jp/azure/cosmos-db/mongodb/vcore/vector-search)

Azure では Azure AI Search を使用してベクター検索を実装することが一般的ですが、Azure Cosmos DB for MongoDB vCore を使用することで、よりスケーラブルにすることが可能です。

## アーキテクチャ

```mermaid
flowchart TB

A([GitHub]) -->|push| B
B[[GitHub Actions]] -->|copy| C
C([Azure Storage Account]) -->|crawl| D
D[[Azure Functions]] -->|upsert/delete| E
D[[Azure Functions]] -->|upsert/delete| F
E([Azure Cosmos DB for MongoDB vCore])
F([Azure AI Search])
G[[Web Client]] -->|request| H
H[[Azure Functions]] -->|re-rank| I
I{{RRF}} -->|search| E
I{{RRF}} -->|search| F
```
