cd ..

npx create-nx-workspace@latest allors --preset=empty --cli=nx --nx-cloud=false

cd allors

npm install -D jest-chain
npm install -D jest-trx-results-processor
npm install -D @nrwl/angular

npm install @angular/cdk
npm install @angular/material
npm install @angular/material-luxon-adapter
npm install bootstrap@4.6.0
npm install common-tags
npm install cross-fetch
npm install date-fns
npm install easymde
npm install jsnlog
npm install luxon

// Base
npx nx g @nrwl/angular:application base/workspace/angular/foundation-app --routing=true --e2eTestRunner=none
npx nx g @nrwl/angular:application base/workspace/angular-material/application-app --routing=true --style=scss --e2eTestRunner=none
npx nx g @nrwl/workspace:library base/workspace/angular/foundation
npx nx g @nrwl/workspace:library base/workspace/angular/application
npx nx g @nrwl/workspace:library base/workspace/angular-material/foundation
npx nx g @nrwl/workspace:library base/workspace/angular-material/application
npx nx g @nrwl/workspace:library base/workspace/domain
npx nx g @nrwl/workspace:library base/workspace/meta
npx nx g @nrwl/workspace:library base/workspace/meta-json

// Core
npx nx g @nrwl/workspace:library core/workspace/domain
npx nx g @nrwl/workspace:library core/workspace/meta
npx nx g @nrwl/workspace:library core/workspace/meta-json

// System
npx nx g @nrwl/workspace:library system/common/protocol-json

npx nx g @nrwl/workspace:library system/workspace/adapters
npx nx g @nrwl/workspace:library system/workspace/adapters-tests
npx nx g @nrwl/workspace:library system/workspace/adapters-json
npx nx g @nrwl/workspace:library system/workspace/adapters-json-tests

npx nx g @nrwl/workspace:library system/workspace/domain
npx nx g @nrwl/workspace:library system/workspace/meta
npx nx g @nrwl/workspace:library system/workspace/meta-tests
npx nx g @nrwl/workspace:library system/workspace/meta-json
npx nx g @nrwl/workspace:library system/workspace/meta-json-tests
