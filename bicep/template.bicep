param storage_account_name string

param cosmosdb_mongodb_name string
param cosmosdb_mongodb_username string
@secure()
param cosmosdb_mongodb_password string

param ai_search_name string

param api_management_name string
param api_management_publisher_email string
param api_management_publisher_name string

param location string = resourceGroup().location

resource storage_account 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storage_account_name
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    dnsEndpointType: 'Standard'
    defaultToOAuthAuthentication: false
    publicNetworkAccess: 'Enabled'
    allowCrossTenantReplication: true
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
    allowSharedKeyAccess: false
    supportsHttpsTrafficOnly: true
    accessTier: 'Hot'
  }
}

resource storage_account_blob_service 'Microsoft.Storage/storageAccounts/blobServices@2023-01-01' = {
  parent: storage_account
  name: 'default'
}

resource storage_account_file_service 'Microsoft.Storage/storageAccounts/fileServices@2023-01-01' = {
  parent: storage_account
  name: 'default'
}

resource storage_account_queue_service 'Microsoft.Storage/storageAccounts/queueServices@2023-01-01' = {
  parent: storage_account
  name: 'default'
}

resource storage_account_table_service 'Microsoft.Storage/storageAccounts/tableServices@2023-01-01' = {
  parent: storage_account
  name: 'default'
}

resource storage_account_blob_container_web 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-01-01' = {
  parent: storage_account_blob_service
  name: '$web'
}

resource storage_account_blob_container_zenn 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-01-01' = {
  parent: storage_account_blob_service
  name: 'zenn'
}

resource cosmosdb_mongodb 'Microsoft.DocumentDB/mongoClusters@2023-09-15-preview' = {
  name: cosmosdb_mongodb_name
  location: location
  properties: {
    administratorLogin: cosmosdb_mongodb_username
    administratorLoginPassword: cosmosdb_mongodb_password
    serverVersion: '6.0'
    nodeGroupSpecs: [
      {
        kind: 'Shard'
        sku: 'Free'
        diskSizeGB: 32
        enableHa: false
        nodeCount: 1
      }
    ]
  }
}

resource ai_search 'Microsoft.Search/searchServices@2024-03-01-preview' = {
  name: ai_search_name
  location: location
  sku: {
    name: 'free'
  }
  properties: {
    replicaCount: 1
    partitionCount: 1
  }
}

resource api_management 'Microsoft.ApiManagement/service@2023-05-01-preview' = {
  name: api_management_name
  location: location
  sku: {
    name: 'Consumption'
    capacity: 0
  }
  properties: {
    publisherEmail: api_management_publisher_email
    publisherName: api_management_publisher_name
  }
}
