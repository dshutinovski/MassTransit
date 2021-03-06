// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Testing.Instances
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Saga;
    using Subjects;
    using TestActions;


    public class SagaTestInstance<TScenario, TSaga> :
        TestInstance<TScenario>,
        SagaTest<TScenario, TSaga>
        where TSaga : class, ISaga
        where TScenario : IBusTestScenario
    {
        readonly SagaTestSubjectImpl<TScenario, TSaga> _subject;

        bool _disposed;

        public SagaTestInstance(TScenario scenario, IList<ITestAction<TScenario>> actions, ISagaRepository<TSaga> sagaRepository)
            : base(scenario, actions)
        {
            _subject = new SagaTestSubjectImpl<TScenario, TSaga>(sagaRepository);
        }

        public SagaTestSubject<TSaga> Saga => _subject;

        public override async Task DisposeAsync()
        {
            if (_disposed)
                return;

            await _subject.DisposeAsync().ConfigureAwait(false);

            await base.DisposeAsync().ConfigureAwait(false);

            _disposed = true;
        }
    }
}