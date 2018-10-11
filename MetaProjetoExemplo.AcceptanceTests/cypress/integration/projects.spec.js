context('Project Management', () => {
  it('Testa adiciona projeto com data valida', () => {
    // custom command
    cy.login()
    cy.server()
    cy.route('/api/project-management').as('projects')

    cy.wait('@projects')
    cy.get('input[name="title"]').type('testing')
    // cy.get('input[name="startDate"]').clear().type('10102020')
    // cy.get('input[name="finishDate"]').clear().focus().type('10102020')
    // cy.wait(1000)


    cy.get('input[name="finishDate"]')
      .as('finishDate')
      .then($input => {
      // get native DOM element
        $input = $input.get(0);

        // track last value since we need it for React16 track value
        const lastValue = $input.value;

        $input.value = '29102018';

        // dispatch change into React as well
        $input._valueTracker.setValue(lastValue);
        $input.dispatchEvent(new Event('change', { bubbles: true }));
      })
    cy.get('input[name="startDate"]')
      .as('startDate')
      .then($input => {
    // get native DOM element
      $input = $input.get(0);

      // track last value since we need it for React16 track value
      const lastValue = $input.value;

      $input.value = '29102018';

      // dispatch change into React as well
      $input._valueTracker.setValue(lastValue);
      $input.dispatchEvent(new Event('change', { bubbles: true }));
    })

    // cy.contains('button', 'Criar').click();
  })
})