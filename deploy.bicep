param location string = resourceGroup().location
param appServicePlanName string = 'myAppServicePlan'
param webAppName string = 'myWebApp'
param skuName string

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: skuName
  }
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2021-02-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      appSettings: [
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'SCM_BASIC_AUTH'
          value: 'true'
        }
      ]
      linuxFxVersion: 'DOTNETCORE|8.0'
    }
  }
}
